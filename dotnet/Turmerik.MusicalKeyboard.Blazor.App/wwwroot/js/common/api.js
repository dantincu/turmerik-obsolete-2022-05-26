import { Trmrk as trmrk } from './core.js';

trmrk.api = {
    baseUrisMapLocalStorageKey: "trmrk-api-base-uris-map",
    baseUrisMap: {},
    init: () => {
        let json = localStorage.getItem(trmrk.api.baseUrisMapLocalStorageKey) || "{}";
        let map = JSON.parse(json);

        trmrk.api.baseUrisMap = map;
        return map;
    },
    updateLocalStorageKey: (map) => {
        let json = JSON.stringify(map);
        localStorage.setItem(trmrk.api.baseUrisMapLocalStorageKey, json);
    },
    setBaseUrisMap: map => {
        trmrk.api.baseUrisMap = map;
        trmrk.api.updateLocalStorageKey(map);
    },
    addBaseUri: (key, baseUri) => {
        trmrk.api.baseUrisMap[key] = baseUri;
        trmrk.api.updateLocalStorageKey(trmrk.api.baseUrisMap);

        return trmrk.api.baseUrisMap;
    },
    addBaseUrisMap: (map) => {
        for (let key in map) {
            let value = map[key];
            trmrk.api.baseUrisMap[key] = value;
        }

        trmrk.api.updateLocalStorageKey(trmrk.api.baseUrisMap);
        return trmrk.api.baseUrisMap;
    },
    getApiUri: (apiBaseUri, apiRelUri) => {
        apiRelUri = apiRelUri ?? "";
        apiRelUri = trmrk.strTrimStart(apiRelUri, '/');

        apiBaseUri = trmrk.strTrimEnd(apiBaseUri, '/');
        let apiUri = apiBaseUri + '/' + apiRelUri;

        return apiUri;
    },
    fetch: async (apiKey, apiRelUri, isPost, jsonData) => {
        let apiUri = "";
        let apiBaseUri = trmrk.api.baseUrisMap[apiKey];

        let responseWrapper = {};

        if (typeof (apiBaseUri) == "string") {
            apiUri = trmrk.api.getApiUri(apiBaseUri, apiRelUri);

            let init = {
                credentials: "include"
            };

            if (isPost) {
                init.method = "POST";
                init.headers = { 'Content-Type': 'application/json' };

                if (typeof (jsonData) == "string") {
                    init.body = jsonData;
                }
            }

            responseWrapper = await fetch(apiUri, init);

            let response = await responseWrapper.json();
            responseWrapper.response = response;
        } else {
            responseWrapper.apiBaseUriNotSet = true;
        }

        return responseWrapper;
    },
    fetchGet: async (apiKey, apiRelUri) => {
        let response = await trmrk.api.fetch(
            apiKey, apiRelUri);

        return response;
    },
    fetchPost: async (apiKey, apiRelUri, jsonData) => {
        let response = await trmrk.api.fetch(
            apiKey, apiRelUri, true, jsonData);

        return response;
    }
};