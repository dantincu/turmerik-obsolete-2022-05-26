const trmrk = {
    uiBlockingOverlayId: "ui-blocking-overlay",
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
    },
    showUIBlockingOverlay: () => {
        trmrk.addCssClass(trmrk.uiBlockingOverlayId, null, "trmrk-hidden");
    },
    hideUIBlockingOverlay: () => {
        trmrk.removeCssClass(trmrk.uiBlockingOverlayId, null, "trmrk-hidden");
    },
    strTrimStart: (str, strToTrim) => {
        while (str.startsWith(strToTrim)) {
            str = str.substring(strToTrim.length);
        }

        return str;
    },
    strTrimEnd: (str, strToTrim) => {
        while (str.endsWith(strToTrim)) {
            str = str.substring(0, str.length - strToTrim.length);
        }

        return str;
    }
}

window.Trmrk = trmrk;
export const Trmrk = trmrk;