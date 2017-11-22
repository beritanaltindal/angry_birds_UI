#pragma strict

var durdur : boolean = false;

function OnGUI() {
if(Input.GetKeyDown(KeyCode.Escape)) {
durdur = true;
}

if(durdur ==true){
GUI.Box(Rect(310,75,300,400),"Oyun Durduruldu");
}

if(GUI.Button(Rect(420,100,80,20),"Devam")){
durdur = false; 
}


if(GUI.Button(Rect(420,130,50,20),"Çıkış")){
Application.Quit();
}

if(durdur == true){
Time.timeScale = 0;

}

if(durdur == false ){
Time.timeScale = 1;
}

}



