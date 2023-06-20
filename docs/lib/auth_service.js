import { AuthResponse } from "./auth_response.js";

class AuthService {

    /** @type {(input: RequestInfo | URL, requestUnit: RequestInit | undefined) => Promise<Response>} */
    #fetch;
    get fetch() { return this.#fetch; }

    /** @type {string} */
    #uri;
    get uri() { return this.#uri; }

    /**
     * @param {Response} message 
     * @returns {LinkerResponse}
     */
    async #handleResponse(message) {
        try {
            let response = new AuthResponse(await message.json());
            return response;
        } catch {
            let response = new AuthResponse();
            response.errors = [message.status, message.statusText?.trim().toUpperCase().replace(" ", "_") ?? "NO_ERROR"];
            return response;
        }
    }

    /**
     * @param {object} credential 
     * @returns {AuthResponse}
     */
    async accessAsync(credential) {
        if (typeof credential != "object") throw new Error("Exptected credential object");
        var uri = `${this.#uri}/access`;
        let json = JSON.stringify(credential);
        var message = await this.#fetch(uri, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: json
        });
        let response = await this.#handleResponse(message);
        return response;
    }

    /**
     * @param {string} refreshToken 
     * @returns {LinkerResponse}
     */
    async refreshAsync(refreshToken) {
        if (typeof refreshToken != "string") throw new Error("Expected refres token");
        var uri = `${this.#uri}/refresh`;
        var message = await this.#fetch(uri, {
            method: "POST",
            headers: {
                "Authorization": `bearer ${refreshToken}`
            }
        });
        let response = await this.#handleResponse(message);
        return response;
    }

    /**
     * @param {(input: RequestInfo | URL, requestUnit: RequestInit | undefined) => Promise<Response>} fetch 
     * @param {string} uri 
     */
    constructor(fetch, uri) {
        if (typeof fetch != "function") throw new Error("The fetch must be a function");
        if (typeof uri != "string") throw new Error("The uri must be a string");
        this.#fetch = fetch;
        this.#uri = uri;
    }

}

export {
    AuthService
}