"""
C# File merger for RageMP.

Copyright (c) 2020-2022 Hannele Ruiz
Under the MIT License
"""
import logging
import re
import sys
from datetime import datetime
from pathlib import Path

HEADER = "// LemonUI for RageMP\n// Generated on {date}{comment}\n#define RAGEMP\n{statements}\n\n{code}\n"
LOGGER = logging.getLogger("merger")

RE_SPECIFIC_USINGS = re.compile("#e?l?if RAGEMP\n([a-zA-Z0-9; . =\n]*)\n(?:#elif|#endif)")
RE_GENERAL_USINGS = re.compile("(?:^|#endif\n)([a-zA-Z0-9; .\n=]*);")
RE_USING = re.compile("using ([A-Za-z0-9.= ]+);?", re.MULTILINE)
RE_NAMESPACE = re.compile("(namespace LemonUI[.A-Za-z /]*\n{[\\s\\S]*})")
RE_SKIP = re.compile("// NO MERGE")


def process_usings(match: re.Match):
    if match is None:
        return

    for main_group in match.groups():
        for statement_group in RE_USING.finditer(main_group):
            if statement_group is None:
                continue

            for statement in statement_group.groups():
                yield statement


def main():
    logging.basicConfig(level=logging.INFO)

    arguments = sys.argv

    LOGGER.info("Launch Arguments: " + " ".join(f"\"{x}\"" for x in arguments))

    if len(arguments) > 4 or len(arguments) < 3:
        LOGGER.critical("Expected 2 or 3 arguments: [code source] [code output] {comment}")
        sys.exit(1)

    code_path = Path(arguments[1])
    code_output = Path(arguments[2])
    comment = f"\n// {arguments[3]}" if len(arguments) > 3 else ""

    strings = ""
    namespaces = []

    LOGGER.info("Starting the processing of the files")

    for path in code_path.rglob("*.cs"):
        with path.open(encoding="utf-8") as file:
            content = file.read()

        relative_path = path.relative_to(code_path)

        if RE_SKIP.match(content):
            LOGGER.info(f"Ignoring {relative_path}: File marked as to be skipped")
            continue

        LOGGER.info(f"Processing {relative_path}")

        namespaces_found = RE_NAMESPACE.search(content).groups()

        if namespaces_found is None:
            LOGGER.warning("File does not contains usable code")
            continue

        specific = list(process_usings(RE_SPECIFIC_USINGS.search(content)))
        general = list(process_usings(RE_GENERAL_USINGS.search(content)))

        LOGGER.info(f"Found {len(specific)} + {len(general)} using statements")

        namespaces.extend(specific)
        namespaces.extend(general)

        namespace = namespaces_found[0]
        strings = f"{strings}\n\n// {relative_path}\n{namespace}"

    namespaces = list(set(namespaces))
    namespaces.sort()

    LOGGER.info(f"Adding {len(namespaces)} unique using statements: " + ", ".join(namespaces))

    statements = "\n".join(f"using {x};" for x in namespaces)

    code_output.parent.mkdir(exist_ok=True)
    with open(code_output, mode="w", encoding="utf-8") as file:
        text = HEADER.format(date=datetime.now(), statements=statements, code=strings, comment=comment)
        file.write(text)

    LOGGER.info(f"File exported as {code_output}")


if __name__ == "__main__":
    main()
