var OpenWindowPlugin = {
    OpenWindow: function(url, width, height) {
        url = Pointer_stringify(url);
        window.open(url, '_blank', 'width=' + width + ', height=' + height);
    }
};

mergeInto(LibraryManager.library, OpenWindowPlugin);