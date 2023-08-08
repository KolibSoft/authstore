class CredentialModel {

    /** @type {string} */
    id;

    /** @type {string} */
    identity;

    /** @type {string} */
    key;

    /** @type {boolean} */
    active;

    /** @type {Date} */
    updatedAt;

    constructor(json) {
        this.id = json?.id ?? crypto.randomUUID();
        this.identity = json?.identity ?? "";
        this.key = json?.key ?? "";
        this.active = json?.active ?? true;
        this.updatedAt = new Date(json?.updatedAt ?? new Date());
    }

}

export {
    CredentialModel
}