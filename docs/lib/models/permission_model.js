class PermissionlModel {

    /** @type {string} */
    id;

    /** @type {string} */
    code;

    /** @type {boolean} */
    active;

    /** @type {Date} */
    updatedAt;

    constructor(json) {
        this.id = json?.id ?? crypto.randomUUID();
        this.code = json?.code ?? "";
        this.active = json?.active ?? true;
        this.updatedAt = new Date(json?.updatedAt ?? new Date());
    }

}

export {
    PermissionlModel
}