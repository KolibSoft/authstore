import { Item } from "../modules/catalogue.js";

class CredentialModel extends Item {

    /** @type {string} */
    identity;

    /** @type {string} */
    key;

    /** @type {boolean} */
    active;

    constructor(json) {
        super(json);
        this.identity = json?.identity ?? "";
        this.key = json?.key ?? "";
        this.active = json?.active ?? true;
    }

}

export {
    CredentialModel
}