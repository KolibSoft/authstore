import { Catalogue } from "./modules/catalogue.js"

class CredentialPermissionService extends Catalogue.CatalogueService {
    constructor(fetch, uri) {
        super(fetch, uri);
    }
}

export {
    CredentialPermissionService
}