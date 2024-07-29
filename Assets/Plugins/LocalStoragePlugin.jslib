mergeInto(LibraryManager.library, {
    GetTokenFromLocalStorage: function() {
        var token = localStorage.getItem('token');
        if (!token) token = ""; // Ensure we return a valid string if token is null
        var lengthBytes = lengthBytesUTF8(token) + 1;
        var stringOnWasmHeap = _malloc(lengthBytes);
        stringToUTF8(token, stringOnWasmHeap, lengthBytes);
        return stringOnWasmHeap;
    }
});
