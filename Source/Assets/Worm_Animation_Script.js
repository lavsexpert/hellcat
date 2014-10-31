#pragma strict
function Update () {
    if (Input.GetKeyDown ("up")) {
       animation.CrossFade ("Take 002");
       }
  
     if (Input.GetKeyDown ("down")) {
       animation.CrossFade ("Take 003");
       }
       
         if (Input.GetKeyDown ("space")) {
       animation.CrossFade ("Take 001");
       }
}