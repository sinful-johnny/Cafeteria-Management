window.dragDropInterop = {
    setDragTableData: function (elementId, ShapeId, ShapeName, Width, Height, Radius, ShapeType, type) {
        const draggable = document.getElementById(elementId);

        const json = JSON.stringify(
            {
                ShapeId: ShapeId,
                ShapeName: ShapeName,
                Width: Width,
                Height: Height,
                Radius: Radius,
                ShapeType: ShapeType,
                type: type
            }
        );
        draggable.addEventListener('dragstart', function (event) {
            event.dataTransfer.setData("application/json", json);
        });
    },

    initializeDropzone: function (dropzoneId, dotNetHelper) {
        const dropzone = document.getElementById(dropzoneId);

        dropzone.addEventListener('dragover', function (event) {
            event.preventDefault();
        });

        dropzone.addEventListener('drop', function (event) {
            event.preventDefault();
            const data = JSON.parse(event.dataTransfer.getData("application/json"));
            //const data = event.dataTransfer.getData("application/json");
            if (data.type === "table") {
                delete data.type;
                dotNetHelper.invokeMethodAsync('HandleDropTableData', data, event.clientX, event.clientY);
            }
           
        });
    }
};
