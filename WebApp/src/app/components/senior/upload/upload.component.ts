import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {RestServiceService} from '../../../service/rest-service.service';
import {ActivatedRoute, Router} from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  localUrl: any[];
  id: string = this.route.snapshot.params['id'];
  @Input() type: string;
  fd: FormData;
  selectedFile: any;
  loading: string = "";
  check: any;
  addPicture: string = "/images/data";
  addVideo: string = "/videos/data";
  @Output() reload = new EventEmitter();

  constructor(private restService: RestServiceService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
  }

  /**
   * Observer event if anything changes
   * @param event
   */
  onFileSelected(event: any) {

    //this.loading = "Upload";

    this.selectedFile = <any>event.target.files;

    console.log(this.selectedFile.length);

    for (let i = 0; i < this.selectedFile.length; i++) {
      if (event.target.files && event.target.files[i]) {
        let reader = new FileReader();
        reader.onload = (event: any) => {
          this.localUrl = event.target.result;
        };
        reader.readAsDataURL(event.target.files[i]);
        $('.preview').append('test');

      }
    }


    //$('.preview').html('<img [src]="localUrl" *ngIf="localUrl" class="imgPlaceholder">');

  }

  /**
   *
   * Upload selected file as formdata either to image or video depending on this.selectefFile[index].type --> image or video
   * Loop through all selectedfiles
   *
   */

  async onUpload() {
    for (const file in this.selectedFile) {
      const index = parseInt(file);
      if (!isNaN(index)) {
        this.loading = "uploading...";
        const fd = new FormData();
        fd.append("File", this.selectedFile[index], this.selectedFile[index].name);
        if (this.selectedFile[index].type.indexOf("image") != -1) {
          this.check = await this.restService.addCorrectMediaToDatabase(this.id, fd, this.addPicture);
        }
        else if (this.selectedFile[index].type.indexOf("video") != -1) {
          this.check = await this.restService.addCorrectMediaToDatabase(this.id, fd, this.addVideo);
        }
        else{
          alert("won't work");
        }

      }

      $("#addMedia").modal("close");

    }

    //clear selected files
    this.selectedFile = null;
    if (this.check) {
      this.selectedFile = null;
      this.reload.emit();
      //this.loading = "Uploaded!"
    } else{
      this.router.navigate(["/error"]);
    }

  }


  /**
   *
   * Open modal in edit mode and fill modal with resident
   *
   */
  addModal() {
    $("#addMedia").modal();
    $("#addMedia").modal("open");
  }

}