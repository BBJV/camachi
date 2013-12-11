// Copyright Carsten B. Larsen 2007.
// You may copy, use, and modify this script as you see fit
//
// In order to use this script, use a World Particle Collider,
// and set the Collision Energy Loss to be a _negative_ number
// which is numerically greater the burstEnergy below, which should
// be a positive number. 
// burstEnergy should be greater than the maximum energy a particle 
// could get from start, i.e. greater than particleEmitter.maxEnergy
//
// When the particle collides with something, its energy will become
// a large positive number, greater that burstEnergy, and the script
// will kill the particle and replace it with "explosionObject"

var burstEnergy : float = 10.0;
var explosionObject : Transform;
var myTransform : Transform;
var MaxParticleCount : int;
var explosionTransform : Transform[];
var tempPosition : Transform;
var nowIndex : int;
var rainMesh : Mesh;
function Start (){
	myTransform = transform;
	rainMesh = myTransform.GetComponent(MeshFilter).mesh;

	explosionTransform = new Transform[MaxParticleCount];
	
	for(var i = 0; i < MaxParticleCount; i++ ){
		explosionTransform[i] = Transform.Instantiate(explosionObject, 
		    		tempPosition.position,  
		    		Quaternion.identity );
		explosionTransform[i].parent = myTransform;
	}
	
}
var rainPoint : Vector3 ;
var rainHit : RaycastHit;
function LateUpdate () {

	for(var i = 0 ; i < MaxParticleCount ; i++){
		
		rainPoint = myTransform.TransformPoint(
		Random.Range(rainMesh.bounds.center.x - rainMesh.bounds.extents.x,rainMesh.bounds.center.x + rainMesh.bounds.extents.x),
		0.0f,
		Random.Range(rainMesh.bounds.center.z - rainMesh.bounds.extents.z,rainMesh.bounds.center.z + rainMesh.bounds.extents.z)
		);
		if(Physics.Raycast(rainPoint,Vector3.down,rainHit))
		{
			explosionTransform[i].position = rainHit.point;
			explosionTransform[i].particleEmitter.Emit();
		}
	}
/*
	var theParticles = particleEmitter.particles;
	
	var liveParticles = new int[theParticles.length];
	var particlesToKeep = 0;
	
	for (var i = 0; i < particleEmitter.particleCount; i++ )
	{
		if (theParticles[i].energy > burstEnergy)
		{
	    	theParticles[i].color = Color.yellow;
    		// We have so much energy, we must go boom
	    	if (explosionObject)
	    	{
	    		
		    	//explosionTransform = Transform.Instantiate(explosionObject, 
		    	//	theParticles[i].position,  
		    	//	Quaternion.identity );
		    	//	explosionTransform.parent = myTransformansform;
		    		
		    		if(nowIndex >= MaxParticleCount){
		    			nowIndex = 0;
		    		}
		    	explosionTransform[nowIndex].position = theParticles[i].position;
		    	explosionTransform[nowIndex].particleEmitter.Emit();
		    	nowIndex++;
		    	
		    	//Transform.Instantiate(explosionObject, 
		    	//	theParticles[i].position,  
		    	//	Quaternion.identity );
		    		
		    }
		
		} 
		else {
			liveParticles[particlesToKeep++] = i;
		}
		
	}
	
	// Copy the ones we keep to a new array
	var keepParticles = new Particle[particlesToKeep];
	for (var j = 0; j < particlesToKeep; j++)
		keepParticles[j] = theParticles[liveParticles[j]];
	// And write changes back
	particleEmitter.particles = keepParticles;
	*/
}	