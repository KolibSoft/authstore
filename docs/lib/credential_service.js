import { Catalogue } from "./modules/catalogue.js"

class CredentialService extends Catalogue.CatalogueService {
    constructor(fetch, uri) {
        super(fetch, uri);
    }
}

export {
    CredentialService
}