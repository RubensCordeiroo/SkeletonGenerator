import fs from "fs";
import { parseSource } from "./ts-parser.js";

const args = process.argv.slice(2);
if (args.length != 1) {

    console.error("Invalid number of arguments");
    process.exit(1);
}
 
const fullPath = args[0];
if (!fs.existsSync(fullPath)) {
    console.error("The file does not exist");
    process.exit(1);
}

const source = parseSource(fullPath);
const json = JSON.stringify(source, null, 4);
process.stdout.write(json);
process.exit(0);

