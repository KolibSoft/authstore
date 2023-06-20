import { AuthService } from "./auth_service.js";

class AuthClient {

    /** @type {AuthService} */
    #service;
    get service() { return this.#service; }

    /**
     * @param {object} credential 
     * @param {string[]} errors 
     * @returns {boolean}
     */
    async accessAsync(credential, errors) {
        if (errors && !Array.isArray(errors)) throw new Error("The errors must be an array or null");
        let response = await this.#service.accessAsync(credential);
        if (response.errors.length > 0) {
            if (errors) for (let error of response.errors) error.push(error);
            return null;
        }
        return response.auth;
    }

    /**
     * @param {string} refreshToken 
     * @param {string[]} errors 
     * @returns {boolean}
     */
    async refreshAsync(refreshToken, errors) {
        if (errors && !Array.isArray(errors)) throw new Error("The errors must be an array or null");
        let response = await this.#service.refreshAsync(refreshToken);
        if (response.errors.length > 0) {
            if (errors) for (let error of response.errors) error.push(error);
            return null;
        }
        return response.auth;
    }

    /**
     * @param {LinkerService} service 
     */
    constructor(service) {
        this.#service = service;
    }

}

export {
    AuthClient
}