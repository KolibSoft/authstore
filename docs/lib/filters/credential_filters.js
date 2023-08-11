import { CatalogueFilters } from "../modules/catalogue.js";

class CredentialFilters extends CatalogueFilters {

    /** @type {boolean} */
    clean;

    constructor(json) {
        super(json);
        this.clean = json?.clean ?? true;
    }

}

export {
    CredentialFilters
}