
//声明整个Texture的分布情况，4行4列，4个动画
var colCount    : int =  4;
var rowCount    : int =  4;
//声明想要播放的动画起始位置
//比如rowNumber = 0 colNumber = 0  代表红色第一个笑脸
// rowNumber = 3 colNumber = 0 代表黄色第一个笑脸
var rowNumber   : int =  0; //从0开始计算
var colNumber   : int =  0; //从0开始计算
var totalCells  : int =  4;
var fps  : int = 10;
var offset  : Vector2;
//更新动画，传递参数给SetSpriteAnimation（）
function Update () { SetSpriteAnimation(colCount,rowCount,rowNumber,colNumber,totalCells,fps);  }
//设置动画SetSpriteAnimation（贴图总列数，总行数，指定动画起始帧所行号，列号，动画总帧数，帧率）
function SetSpriteAnimation(colCount : int,rowCount : int,rowNumber : int,colNumber : int,totalCells : int,fps : int){
    // 计算索引
    var index : int = Time.time * fps;
    index = index % totalCells;
    // 每个单元大小
    var size = Vector2 (1.0 / colCount, 1.0 / rowCount);
    // 分割成水平和垂直索引
    var uIndex = index % colCount;
    var vIndex = index / colCount;
    //颠倒V，让贴图正过来，所见即所得
    offset = Vector2 ((uIndex+colNumber) * size.x, (1.0 - size.y) - (vIndex+rowNumber) * size.y);
    renderer.material.SetTextureOffset ("_MainTex", offset);
    renderer.material.SetTextureScale  ("_MainTex", size);
}