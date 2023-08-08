import { Catalogue } from "../lib/modules/catalogue.js";

async function fetchHandler(uri, request) {
    return await fetch(uri, request);
}

let uri = "http://localhost:5177";


