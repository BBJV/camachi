// Scroll main texture based on time

var scrollSpeedX : float = 0.5;
var scrollSpeedY : float = 0.5;

function Update() {
    var offsetX : float = Time.time * scrollSpeedX;
    var offsetY : float = Time.time * scrollSpeedY;
    renderer.material.mainTextureOffset = Vector2 (offsetX, offsetY);
}