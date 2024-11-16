window.dragDropInterop = {
    setDragData: function (elementId, data) {
        const draggable = document.getElementById(elementId);
        draggable.addEventListener('dragstart', function (event) {
            event.dataTransfer.setData("text/plain", data);
        });
    },

    initializeDropzone: function (dropzoneId, dotNetHelper) {
        const dropzone = document.getElementById(dropzoneId);

        dropzone.addEventListener('dragover', function (event) {
            event.preventDefault();
        });

        dropzone.addEventListener('drop', function (event) {
            event.preventDefault();
            //const data = JSON.parse(event.dataTransfer.getData("text/plain"));
            const data = event.dataTransfer.getData("text/plain");
            dotNetHelper.invokeMethodAsync('HandleDropData', data, event.clientX, event.clientY);
        });
    }
};
