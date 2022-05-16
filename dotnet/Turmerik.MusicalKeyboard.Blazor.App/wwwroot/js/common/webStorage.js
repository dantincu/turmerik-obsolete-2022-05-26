import { Trmrk as trmrk } from './core.js';

const webStorage = {
    bigItems: {},
    clear: (isPersistent) => {
        trmrk.log("webStorage.clear(isPersistent)", isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        storage.clear();
    },
    containsKey: (key, isPersistent) => {
        trmrk.log("webStorage.clear(key, isPersistent)", key, isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        let len = storage.length;
        let retVal = false;

        for (let i = 0; i < len; i++) {
            let candKey = storage.key(i);

            if (candKey === key) {
                retVal = true;
                break;
            }
        }

        return retVal;
    },
    keys: isPersistent => {
        trmrk.log("webStorage.keys(isPersistent)", isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        let len = storage.length;
        let keysArr = [];

        for (let i = 0; i < len; i++) {
            let key = storage.key(i);
            keysArr.push(key);
        }

        return keysArr;
    },
    getItem: (key, isPersistent) => {
        trmrk.log("webStorage.getItem(key, isPersistent)", key, isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        let retVal = storage.getItem(key);
        return retVal;
    },
    setItem: (key, value, isPersistent) => {
        trmrk.log("webStorage.setItem(key, value, isPersistent)", key, value, isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        storage.setItem(key, value);
    },
    removeItem: (key, isPersistent) => {
        trmrk.log("webStorage.removeItem(key, isPersistent)", key, isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        storage.removeItem(key);
    },
    removeItems: (keysArr, isPersistent) => {
        trmrk.log("webStorage.removeItems(keysArr, isPersistent)", keysArr, isPersistent);
        let storage = trmrk.webStorage.getStorage(isPersistent);

        let len = keysArr.length;

        for (let i = 0; i < len; i++) {
            let key = keysArr[i];
            storage.removeItem(key);
        }
    },
    getStorage: (isPersistent) => {
        trmrk.log("webStorage.getStorage(isPersistent)", isPersistent);
        let storage;

        if (isPersistent) {
            storage = localStorage;
        } else {
            storage = sessionStorage;
        }

        return storage;
    },
    getBigItemChunksCount: (key, guidStr, maxChunkLength, isPersistent) => {
        trmrk.log("webStorage.getBigItemChunksCount(key, guidStr, maxChunkLength, isPersistent)", key, guidStr, maxChunkLength, isPersistent);
        let text = trmrk.webStorage.getItem(key, isPersistent);

        if (typeof (text) !== "string") {
            text = "";
        }

        let lines = trmrk.textToLines(text, maxChunkLength);
        trmrk.webStorage.bigItems[guidStr] = lines;

        let chunksCount = lines.length;
        return chunksCount;
    },
    getBigItemChunk: (guidStr, idx) => {
        let textChunk = Trmrk.webStorage.bigItems[guidStr][idx];
        return textChunk;
    },
    clearBigItemChunks: guidStr => {
        trmrk.log("webStorage.clearBigItemChunks(guidStr)", guidStr);
        delete Trmrk.webStorage.bigItems[guidStr];
    }
}

trmrk.webStorage = webStorage;
export const WebStorage = webStorage;