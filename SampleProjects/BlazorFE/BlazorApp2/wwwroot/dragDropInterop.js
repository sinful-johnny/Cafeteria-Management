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
                Type: type
            }
        );
        draggable.addEventListener('dragstart', function (event) {
            event.dataTransfer.setData("application/json", json);
        });
    },

    setDragFoodData: function (elementId, FoodId, type) {
        const draggable = document.getElementById(elementId);

        const json = JSON.stringify(
            {
                FoodId: FoodId,
                Type: type
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

            //console.log(data);
            if (data.Type === "table") {
                delete data.Type;
                dotNetHelper.invokeMethodAsync('HandleDropTableData', data, event.clientX, event.clientY);
            } else if (data.Type === "food") {
                dotNetHelper.invokeMethodAsync('HandleDropFoodData', data, event.clientX, event.clientY);
            }
        });
    }
};
