// Similar to GL_example1b, but illustrates using a varying variable
// to interpolate a color attribute for each vertex.

//triangle
//-----------------------------------------------------------------------

// vertex shader
const vshaderSource1 = `
attribute vec4 a_Position1;
attribute vec4 a_Color1;
varying vec4 color1;
void main()
{
  color1 = a_Color1;
  gl_Position = a_Position1;

}
`;

// fragment shader
// vertex shader
const fshaderSource1 = `
// In a webgl fragment shader, float precision has to be specified before
// any other variable declarations (in this case, "medium" precision)
precision mediump float;
varying vec4 color1;
void main()
{
  gl_FragColor = color1;
}
`;

// raw data for some point positions
var numPoints1 = 3;
var vertices1 = new Float32Array([
  -1.0, -0.75,
  0.0, -0.75,
  -0.5, 0.75
// -0.5, -0.75,
// 0.5, -0.75,
// 0.0, 0.75
]
);

// a color value for each vertex
var colors1 = new Float32Array([
1.0, 0.0, 0.0, 1.0,  // red
0.0, 1.0, 0.0, 1.0,  // green
0.0, 0.0, 1.0, 1.0,  // blue
]
);


// A few global variables...

// the OpenGL context
var gl;

// handles to buffers on the GPU
var vertexbuffer1;
var colorbuffer1;

// handle to the compiled shader program on the GPU
var shader1;

//square
//--------------------------------------

//vertex shader
const vshaderSource = `
attribute vec4 a_Position;
void main() {
  gl_Position = a_Position;
}
`;

// fragment shader
const fshaderSource = `
// precision declaration required to use floats
precision mediump float;
uniform vec4 color;
void main()
{
  //gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
	gl_FragColor = color;
}
`;

// Raw data for some point positions - this will be a square, consisting
// of two triangles.  We provide two values per vertex for the x and y coordinates
// (z will be zero by default).
var numPoints = 6;
var vertices = new Float32Array([
0.0, -0.5,
1.0, -0.5,
1.0, 0.5,
0.0, -0.5,
1.0, 0.5,
0.0, 0.5
]
);

color = [1.0, 1.0, 1.0, 1.0];
// A few global variables...

// the OpenGL context
// var gl;

// handle to a buffer on the GPU
var vertexbuffer;

// handle to the compiled shader program on the GPU
var shader;



// code to actually render our geometry
function drawT()
{

  // clear the framebuffer
  gl.clear(gl.COLOR_BUFFER_BIT);

  // bind the shader
  gl.useProgram(shader1);

  // bind the buffer
  gl.bindBuffer(gl.ARRAY_BUFFER, vertexbuffer1);

  // get the index for the a_Position attribute defined in the vertex shader
  var positionIndex1 = gl.getAttribLocation(shader1, 'a_Position1');
  if (positionIndex1 < 0) {
    console.log('Failed to get the storage location of a_Position1');
    return;
  }

  // "enable" the a_position attribute
  gl.enableVertexAttribArray(positionIndex1);

  // associate the data in the currently bound buffer with the a_position attribute
  // (The '2' specifies there are 2 floats per vertex in the buffer.  Don't worry about
  // the last three args just yet.)
  gl.vertexAttribPointer(positionIndex1, 2, gl.FLOAT, false, 0, 0);

  // we can unbind the buffer now (not really necessary when there is only one buffer)
  gl.bindBuffer(gl.ARRAY_BUFFER, null);

  // bind the buffer with the color data
  gl.bindBuffer(gl.ARRAY_BUFFER, colorbuffer1);

  // get the index for the a_Color attribute defined in the vertex shader
  var colorIndex1 = gl.getAttribLocation(shader1, 'a_Color1');
  if (colorIndex1 < 0) {
    console.log('Failed to get the storage location of a_Color1');
    return;
  }

  // "enable" the a_Color attribute
  gl.enableVertexAttribArray(colorIndex1);

  // Associate the data in the currently bound buffer with the a_Color attribute
  // The '4' specifies there are 4 floats per vertex in the buffer
  gl.vertexAttribPointer(colorIndex1, 4, gl.FLOAT, false, 0, 0);

  // we can unbind the buffer now
  gl.bindBuffer(gl.ARRAY_BUFFER, null);

  // draw, specifying the type of primitive to assemble from the vertices
  gl.drawArrays(gl.TRIANGLES, 0, 3);
  //gl.drawArrays(gl.LINES, 0, 2);

  // unbind shader and "disable" the attribute indices
  // (not really necessary when there is only one shader)
  gl.disableVertexAttribArray(positionIndex1);
  gl.disableVertexAttribArray(colorIndex1);
  gl.useProgram(null);


}

// code to actually render our geometry
function drawS(currentColor)
{
  // clear the framebuffer
  // gl.clear(gl.COLOR_BUFFER_BIT);
console.log(currentColor);
  // bind the shader
  gl.useProgram(shader);

  // bind the buffer
  gl.bindBuffer(gl.ARRAY_BUFFER, vertexbuffer);

  // get the index for the a_Position attribute defined in the vertex shader
  var positionIndex = gl.getAttribLocation(shader, 'a_Position');
  if (positionIndex < 0) {
    console.log('Failed to get the storage location of a_Position');
    return;
  }

  // "enable" the a_position attribute
  gl.enableVertexAttribArray(positionIndex);

  // associate the data in the currently bound buffer with the a_position attribute
  // (The '2' specifies there are 2 floats per vertex in the buffer.  Don't worry about
  // the last three args just yet.)
  gl.vertexAttribPointer(positionIndex, 2, gl.FLOAT, false, 0, 0);

  // we can unbind the buffer now (not really necessary when there is only one buffer)
  gl.bindBuffer(gl.ARRAY_BUFFER, null);

  let index = gl.getUniformLocation(shader, "color");
  // two ways to do this...
  // a) pass four floats individually
  //gl.uniform4f(index, currentColor[0], currentColor[1], currentColor[2], currentColor[3]);
  // or b) since currentColor is a JS array, we can use it to construct a Float32Array
  gl.uniform4fv(index, new Float32Array(currentColor));

  // draw, specifying the type of primitive to assemble from the vertices
  gl.drawArrays(gl.TRIANGLES, 0, numPoints);

  // unbind shader and "disable" the attribute indices
  // (not really necessary when there is only one shader)
  gl.disableVertexAttribArray(positionIndex);
  gl.useProgram(null);

}

function logPosition(event)
{
  let canvas = document.getElementById('theCanvas');
  let rect = canvas.getBoundingClientRect();
  let x = event.clientX - rect.left;
  let y = event.clientY - rect.top;

  // reverse so (0, 0) is lower left corner, to be consistent with framebuffer
  y = canvas.height - y;
  console.log(x + ", " + y);

  //if within triangle

  drawS(findRGB(x, y, 200, 300, colors1));
}

/**
 * Represents an RGBA color. Values should normally be in the range [0.0, 1.0].
 * @constructor
 * @param {Number} r - red value (default 0.0)
 * @param {Number} g - green value (default 0.0)
 * @param {Number} b - blue value (default 0.0)
 * @param {Number} a - alpha value (default 1.0)
 */
function Color(r, g, b, a)
{
	this.r = (r ? r : 0.0);
	this.g = (g ? g : 0.0);
	this.b = (b ? b : 0.0);
	this.a = (a ? a : 1.0);
}

/**
 * Interpolates a color value within an isoceles triangle based on an
 * x, y offset from the lower left corner.  The base of the triangle is
 * always aligned with the bottom of the canvas.  Returns null if the given
 * offset does not lie within the rectangle.
 * @param {Number} x - offset from left side
 * @param {Number} y - offset from bottom
 * @param {Number} base - base of triangle
 * @param {Number} height - height of triangle
 * @param {Color[]} colors - colors of the three corners, counterclockwise
 *   from lower left
 * @return {Color} interpolated color at offset (x, y)
 */
function findRGB(x, y, base, height, colors)
{



  var xv1 = 100;
  var xv2 = 0;
  var xv3 = 200;
  var yv1 = 350;
  var yv2 = 50;
  var yv3 = 50;

  var denom = (yv2-yv3)*(xv1-xv3)+(xv3-xv2)*(yv1-yv3);
  w1 = ((yv2-yv3)*(x-xv3)+(xv3-xv2)*(y-yv3))/denom;
  w2 = ((yv3-yv1)*(x-xv3)+(xv1-xv3)*(y-yv3))/denom;
  w3 = 1 - w1;
  w3 -= w2;



  var red = w2*colors[0]+w3*colors[4] + w1*colors[8];
  var green = w2*colors[1]+w3*colors[5] + w1*colors[9];
  var blue = w2*colors[2]+w3*colors[6] + w1*colors[10];

  if((w1 >= 0) && (w2 >= 0) && (w3 >= 0)){
    color = [red, green, blue, 1.0];
  }

  // color = [red, green, blue, 1.0];
  // var colors1 = new Float32Array([
  // 1.0, 0.0, 0.0, 1.0,  // red
  // 0.0, 1.0, 0.0, 1.0,  // green
  // 0.0, 0.0, 1.0, 1.0,  // blue
  // ]
  // );

  return color;


}

// entry point when page is loaded
function main() {

  // basically this function does setup that "should" only have to be done once,
  // while draw() does things that have to be repeated each time the canvas is
  // redrawn

    // get graphics context
  gl = getGraphicsContext("theCanvas");


//Init Triangle:
//-----------------------------------------------------------------
  // load and compile the shader pair
  shader1 = createShaderProgram(gl, vshaderSource1, fshaderSource1);

  // load the vertex data into GPU memory
  vertexbuffer1 = createAndLoadBuffer(vertices1);

  //load the color data into GPU memory
  colorbuffer1 = createAndLoadBuffer(colors1);


  // specify a fill color for clearing the framebuffer
  gl.clearColor(1.0, 1.0, 1.0, 1.0);

  // we could just call draw() once to see the result, but setting up an animation
  // loop to continually update the canvas makes it easier to experiment with the
  // shaders
  //draw();


//init square
//--------------------------------------------------------------------
// load and compile the shader pair
 shader = createShaderProgram(gl, vshaderSource, fshaderSource);

 // load the vertex data into GPU memory
 vertexbuffer = createAndLoadBuffer(vertices);

 // specify a fill color for clearing the framebuffer
 // gl.clearColor(0.0, 0.8, 0.8, 1.0);


  // define an animation loop
  var animate = function() {
    // retrieve <canvas> element
    var canvas = document.getElementById('theCanvas');

    // attach a mouse click handler
    canvas.onclick = logPosition;
    //draw triangle
	   drawT();

  // draw square:
  	  drawS(color);

	// request that the browser calls animate() again "as soon as it can"
    requestAnimationFrame(animate);
  };

  // start drawing!
  animate();


}
