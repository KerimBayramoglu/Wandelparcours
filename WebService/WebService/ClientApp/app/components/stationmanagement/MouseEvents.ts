import {StationmanagementComponent} from "./stationmanagement.component"
import {getBaseUrl} from "../../app.module.browser";
import {bufferelement, RenderBuffer} from "./RenderBuffer";
import {Position} from "../../models/station"
declare var $: any
export class MouseEvents{
    station:StationmanagementComponent;
    canvas : HTMLCanvasElement;
    clicked:boolean=false;
    position:Point;
    screenPos:Point;
    mousePos:Point;
    mapPos:any;
    
    x=0;
    zoomfactor=1;
    mousepos:any;
    constructor(station:StationmanagementComponent){
        //load in variables from the stationmanagement component
        this.station=station;
        this.canvas=this.station.canvas;
        
        //ad listeners
        this.canvas.addEventListener("mousemove",(ev => this.mouseMove(ev)));
        this.canvas.addEventListener("contextmenu",ev =>  this.rightClick(ev));
        this.canvas.addEventListener("mousedown", ev =>  this.mouseDown(ev));
        this.canvas.addEventListener("mouseup",ev => this.mouseUp(ev));
        //instantiete posistion
        this.position={x:0,y:0};
        this.screenPos={x:0,y:0};
        this.mapPos={x:0,y:0,width:0,height:0};
        
        
    }
  

    async mouseMove(e:MouseEvent){
        //while holding mouseclick
        if (this.clicked){
            //calculate the difference of the x/y location of the mouse compared to the last frame
            this.position.x= this.position.x- (this.screenPos.x-e.screenX);
            this.position.y= this.position.y- (this.screenPos.y-e.screenY);
            //reset te x/y location for the next frame
            this.screenPos = {x:e.screenX,y:e.screenY};
            //update the frame
            this.station.tick();
        }
        
        this.mousepos={x:e.x,y:e.y};
        if (this.station.adMarker){
            this.station.tick();
        }
        
        
    }
    
    
    async collisionDetectionWithClass(className:string){
        let buffer = this.station.renderBuffer.buffer;
        let bufferElement:bufferelement;
        let mousepos=this.mousepos;
        if (mousepos==undefined) return null;
        
        for (bufferElement of buffer){
            if (!(bufferElement.className==className)) continue;
            
            if (
                //check if mouse position is coliding in x axis
                (mousepos.x> bufferElement.x && mousepos.x< (bufferElement.x+bufferElement.width))
                &&
                // check if mouse position is coliding in y axis
                (mousepos.y> bufferElement.y && mousepos.y< (bufferElement.y+bufferElement.height))
                
            )
            {
                
                return bufferElement;
            }
        }
        
        
        
        return null;
    }
    
    async calculateMousePosOnImage(mousePos:Point){
        let x=(mousePos.x-this.mapPos.x)/this.mapPos.width;
        let y =(mousePos.y-this.mapPos.y)/this.mapPos.height;
        let mousepos: Point= {x:x,y:y};
        return mousepos;
    }

     calculateStationPosOnImage(position:Position){
        let point:Point= {
            x: (position.x*this.mapPos.width) + this.mapPos.x ,
            y:(position.y*this.mapPos.height) + this.mapPos.y 
        };
        return point;
        
    }
    
    async mouseUp(e:MouseEvent){
        //disable tracking
        this.clicked=false;
        
        
    }
    async mouseDown(e:MouseEvent){
        
        
        //start calcutating mapmoving if admarker button isn't clicked
        if (!this.station.adMarker){
            
            this.mousepos={x:e.x,y:e.y};
            this.station.collidingElement= await this.collisionDetectionWithClass("marker");
            
            //check if coliding, if so show box message on click
            if (this.station.collidingElement!=null) {
                this.station.deleteModal();
                return;
            }
            //start tracking
            this.clicked=true;
            //set the x/y of current for comparison on the next frame
            this.screenPos = {x:e.screenX,y:e.screenY};
        }else{
            let mouseposition = await this.calculateMousePosOnImage({x:e.layerX,y:e.layerY});
            this.station.saveStationToDatabaseModal(mouseposition)
            
        }
    }
    
    async rightClick(e:MouseEvent){
        //prevent right clicks on the map
        e.preventDefault();
    }
    
    
}

export interface Point{
    x:number;
    y:number;
}

