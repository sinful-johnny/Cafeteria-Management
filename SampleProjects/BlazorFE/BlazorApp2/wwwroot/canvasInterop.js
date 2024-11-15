window.canvasInterop = {
    initialize: function (canvas, dotNetHelper) {
        canvas.addEventListener('mousedown', (event) => {
            dotNetHelper.invokeMethodAsync('OnMouseDown', event.clientX, event.clientY);
        });

        canvas.addEventListener('mousemove', (event) => {
            dotNetHelper.invokeMethodAsync('OnMouseMove', event.clientX, event.clientY);
        });

        canvas.addEventListener('mouseup', (event) => {
            dotNetHelper.invokeMethodAsync('OnMouseUp', event.clientX, event.clientY);
        });
    },

    getBoundingClientRect: function (canvas) {
        return canvas.getBoundingClientRect();
    },

    clearCanvas: function (canvas, context) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    },

    drawRect: function (canvas, context, x, y, width, height, color) {
        context.fillStyle = color;
        context.fillRect(x, y, width, height);
    },
    getContext2D: function (canvas) {
        return canvas.getContext('2d');
    }
};
