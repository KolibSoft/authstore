import { AuthConnector } from "../abstractions/auth_connector.js";
import { AuthModel } from "../models/auth_model.js";
import { Result } from "../modules/catalogue.js";

/**
 * @implements {AuthConnector}
 */
class AuthService {

    /** @type {(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>} */
    fetch;

    /** @type {string} */
    uri;

    /** @returns {boolean} */
    get available() { return navigator.onLine; }

    /**
     * @param {LoginModel} login 
     * @returns {Promise<Result<AuthModel>>}
     */
    async accessAsync(login) {
        let uri = `${this.uri}`;
        let response = await this.fetch(uri, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(login)
        });
        let result = await Catalogue.ResultUtils.handleResult(response);
        if (result.data) result.data = new AuthModel(result.data);
        return result;
    }

    /**
     * @param {string} id 
     * @param {string} refreshToken 
     * @returns {Promise<Result<AuthModel>>}
     */
    async refreshAsync(id, refreshToken) {
        let uri = `${this.uri}/${id}`;
        let response = await this.fetch(uri, {
            method: "GET",
            headers: {
                "Authorization": `bearer ${refreshToken}`
            }
        });
        let result = await Catalogue.ResultUtils.handleResult(response);
        if (result.data) result.data = new AuthModel(result.data);
        return result;
    }

    /**
     * @param {{(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>}} fetch 
     * @param {string} uri 
     */
    constructor(fetch, uri) {
        this.fetch = fetch;
        this.uri = uri;
    }

}

export {
    AuthService
}