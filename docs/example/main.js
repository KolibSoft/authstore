import { Catalogue } from "../lib/modules/catalogue.js";
import { AuthStoreContext } from "./client.js";

async function fetchHandler(uri, request) {
    return await fetch(uri, request);
}

let uri = "http://localhost:5177";
let context = new AuthStoreContext();
let database = await context.open();
console.log(database);

console.log(await context.set("credential").filter(x => true));
