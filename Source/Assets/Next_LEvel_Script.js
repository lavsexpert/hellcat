#pragma strict

function OnTriggerEnter(tr: Collider)
{

if ( tr.collider.tag == "Player")
{
 Application.LoadLevel ("Demo_Scene_02");
}
}