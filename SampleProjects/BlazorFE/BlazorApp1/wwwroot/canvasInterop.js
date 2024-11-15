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

    clearCanvas: function (canvas) {
        const context = canvas.getContext('2d');
        context.clearRect(0, 0, canvas.width, canvas.height);
    },

    drawRect: function (canvas, x, y, width, height, color) {
        const context = canvas.getContext('2d');
        context.fillStyle = color;
        context.fillRect(x, y, width, height);
    }
};
