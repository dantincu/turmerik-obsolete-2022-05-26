import { Trmrk as trmrk } from './core.js';

trmrk.api = {
    baseUrisMap: {},
    setBaseUrisMap: map => {
        trmrk.api.baseUrisMap = map;
    },
    addBaseUri: (key, baseUri) => {
        trmrk.api.baseUrisMap[key] = baseUri;
    },
    addBaseUrisMap: (map) => {
        for (let key in map) {
            let value = map[key];
            trmrk.api.baseUrisMap[key] = value;
        }
    },
    fetchGet: async (apiKey, apiRelUri) => {
        let apiUri = trmrk.api.baseUrisMap[apiKey] + apiRelUri;

        let init = {
            credentials: "include"
        };

        var response = await fetch(apiUri, init);
        var data = await response.json();

        return data;
    },
    fetchPost: async (apiKey, apiRelUri, jsonData) => {
        let apiUri = trmrk.api.baseUrisMap[apiKey] + apiRelUri;

        let init = {
            credentials: "include",
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: jsonData
        };

        var response = await fetch(apiUri, init);
        var data = await response.json();

        return data;
    }
};