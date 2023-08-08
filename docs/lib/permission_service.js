import { Catalogue } from "./modules/catalogue.js"

class PermissionService extends Catalogue.CatalogueService {
    constructor(fetch, uri) {
        super(fetch, uri);
    }
}

export {
    PermissionService
}