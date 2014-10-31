#pragma strict

function OnTriggerEnter(tr: Collider)
{

if ( tr.collider.tag == "Enemy_Warrior")
{
 Application.LoadLevel ("Game_Over_Scene");
 
}
}