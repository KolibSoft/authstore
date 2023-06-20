import { AuthClient } from "../lib/auth_client.js"
import { AuthService } from "../lib/auth_service.js"
import { CatalogueModule } from "../lib/catalogue_module.js";

let uri = "https://localhost:7118/auth"
let client = new AuthClient(new AuthService((a, b) => fetch(a, b), uri));
let auth = await client.accessAsync({ identity: "ROOT", key: "ROOT" });
console.log(auth);
