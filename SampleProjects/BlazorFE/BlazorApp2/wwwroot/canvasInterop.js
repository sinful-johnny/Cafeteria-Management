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

    drawRect: function (context, x, y, width, height, color) {
        context.fillStyle = color;
        context.fillRect(x, y, width, height);
    },
    drawCircle: function (context, x, y, radius, color) {
        context.beginPath(); // Start a new path 
        context.arc(x, y, radius, 0, 2 * Math.PI); // Draw the circle 
        context.fillStyle = color; // Set the fill color 
        context.fill(); // Fill the circle with the color 
    },
    getContext2D: function (canvas) {
        return canvas.getContext('2d');
    }
};
