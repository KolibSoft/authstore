class LoginModel {

    /** @type {string} */
    identity;

    /** @type {string} */
    key;
    
    constructor(json) {
        this.identity = json?.identity ?? null;
        this.key = json?.key ?? null;
    }

}

export {
    LoginModel
}