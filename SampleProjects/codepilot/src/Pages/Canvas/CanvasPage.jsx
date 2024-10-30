import React, {useRef,useEffect,useState} from "react";

import "./CanvasPage.css"
import { bool } from "yup";

export const CanvasPage = () => {
    const canvasRef = useRef(null);
    const [Canvas,setCanvas] = useState(null);
    const [Pointer, setPointer] = useState(null);

    useEffect(() => {
        const canvas = canvasRef.current;
        if (canvas) {
          canvas.width = window.innerWidth;
          canvas.height = window.innerHeight;

          var pointer = canvas.getContext('2d');
          setPointer(canvas.getContext('2d'));
          //setCanvas(() => {return canvas});
          //setPointer(() => { return canvas.getContext("2d")});

      
          //drawArc(canvas,x,y);
          //console.log(pointer);
            var AnimationID;
          const render = () => {
            if(x === window.innerWidth || x === 0){
              flipper *= -1;
            }
            x = x + 2 * flipper;

            pointer.clearRect(0,0,pointer.canvas.width,pointer.canvas.height);
            pointer.beginPath();
            pointer.arc(x,y,30, 0, Math.PI * 2,false);
            pointer.stroke();
            AnimationID = window.requestAnimationFrame(render);
          }

          //render();

          //return () => window.cancelAnimationFrame(AnimationID);
        }
    },[]);

    const dothings = (canvas) => {
        if(canvas){
            var c = canvas.getContext('2d');
            c.fillStyle = "rgba(205,183,255)"
            c.fillRect(100,100,100,100);
            c.fillStyle = "rgba(25,58,255)"
            c.fillRect(400,100,100,100);
        }
    }

    const drawLine = (canvas) => {
        if(canvas){
            var c = canvas.getContext('2d');
            c.beginPath();
            c.moveTo(50,300);
            c.lineTo(300,100);
            c.lineTo(400,300)
            c.strokeStyle = "blue";
            c.stroke();
        }
    }

    const drawArc = (canvas, x, y) =>{
        var c = canvas.getContext('2d');
        c.beginPath();
        c.arc(x,y,30, 0, Math.PI * 2,false);
        c.stroke();
    }

    var x = 300;
    var y = 300;
    var flipper = 1;
    var AnimationID1;
    const animate = (pointer, x, y) => {
        if(x === window.innerWidth){
            flipper *= -1;
        }
        x = x + 2 * flipper;

        pointer.clearRect(0,0,pointer.canvas.width,pointer.canvas.height);
        pointer.beginPath();
        pointer.arc(x,y,30, 0, Math.PI * 2,false);
        pointer.stroke();

        AnimationID1 = window.requestAnimationFrame(animate);
    }

    //dothings(canvas);

    return (
        <>
            <button width="50px" height="30px" onClick={() => {dothings(Canvas)}}>Pop</button>
            <button width="50px" height="30px" onClick={() => {drawLine(Canvas)}}>Line</button>
            <button width="50px" height="30px" onClick={() => {drawArc(Canvas,300,300)}}>Arc</button>
            <button width="50px" height="30px" onClick={() => {animate(Pointer, x, y)}}>Animate</button>
            <canvas ref={canvasRef} className="MainCanvas">
            
            </canvas>
        </>
    )
}