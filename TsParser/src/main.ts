import fs from "fs";
import { parseSource } from "./ts-parser.js";
import { getSkeleton } from "./sketon-parse.js";
import { env } from "process";

const args = process.argv.slice(2);

if(args.length == 1){

    const fullPath = args[0];
    if(!fs.existsSync(fullPath)){
        console.error("The file does not exist");
        process.exit(1);
    }

    const source = parseSource(fullPath);
    const json = JSON.stringify(source, null, 4);
    process.stdout.write(json);
    process.exit(0);
}

//if DEBUG

if(env.NODE_ENV == "development"){

    const dir =  `D:\\OneDrive\\GitHub\\SneburBR\\Snebur.TS\\src\\Snebur.TS\\src\\Utilidade`;
    const dirDest =  `D:\\OneDrive\\GitHub\\SneburBR\\snebur-ts\\packages\\core\\src\\util\\`;
    
    if(!fs.existsSync(dirDest)){
        console.error("The destination directory does not exist");
        fs.mkdirSync(dirDest);
    }
    
    const files = fs.readdirSync(dir);
    const tsFiles = files.filter((file) => file.endsWith(".ts"));
    
    for (const file of tsFiles) {
        
        const fullPath = `${dir}\\${file}`;
        const source = parseSource(fullPath);
        const skeleton = getSkeleton(source);
    
        const pathDest = `${dirDest}\\${file}`;
        console.log(`Saving file:  ${pathDest}`);
        fs.writeFileSync(pathDest, skeleton);
     
    }
    process.stdin.on("data", process.exit.bind(process, 0));
}





 

