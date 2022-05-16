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
    },
    isLoggingEnabled: false,
    log: function() {
        if (trmrk.isLoggingEnabled) {
            console.log.apply(window, arguments);
        }
    }
}

window.Trmrk = trmrk;
export const Trmrk = trmrk;