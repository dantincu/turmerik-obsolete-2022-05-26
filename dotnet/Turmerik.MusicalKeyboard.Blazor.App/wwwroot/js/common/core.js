const trmrk = {
    throw: (err) => {
        if (typeof (err) !== "object") {
            err = new Error(err);
        }

        throw err;
    },
    readFromClipboardAsync: async () => {
        let text = await navigator.clipboard.readText();
        return text;
    },
    writeToClipboardAsync: async (text) => {
        await navigator.clipboard.writeText(text);
    }
}

export const Trmrk = trmrk;