import { Component, OnInit, Input } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';
import { Photo } from 'src/app/_models/photo';
import { ImageCroppedEvent } from 'ngx-image-cropper';
declare var jQuery: any;
@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit {
  @Input() member: Member;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;
  upload:boolean=false;

  constructor(private accountService: AccountService, private memberService: MembersService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeUploader();
   // File Upload
// 

function ekUpload(){
  function Init() {

    var fileSelect    = document.getElementById('file-upload'),
        fileDrag      = document.getElementById('file-drag'),
        resetButton  = document.getElementById('resetform');

    fileSelect.addEventListener('change', fileSelectHandler, false);

 if(resetButton){resetButton.addEventListener('click', ()=>{
  document.getElementById('file-image').classList.add("hidden");
      document.getElementById('start').classList.remove("hidden");
      document.getElementById('response').classList.add("hidden");
 (<HTMLFormElement>document.querySelector("#file-upload-form")).reset();
}, false);
}
    
    // Is XHR2 available?
    var xhr = new XMLHttpRequest();
    if (xhr.upload) {
      // File Drop
      fileDrag.addEventListener('dragover', fileDragHover, false);
      fileDrag.addEventListener('dragleave', fileDragHover, false);
      fileDrag.addEventListener('drop', fileSelectHandler, false);
    }
  }

  function fileDragHover(e) {
    var fileDrag = document.getElementById('file-drag');

    e.stopPropagation();
    e.preventDefault();

    fileDrag.className = (e.type === 'dragover' ? 'hover' : 'modal-body file-upload');
  }

  function fileSelectHandler(e) {
    // Fetch FileList object
    var files = e.target.files || e.dataTransfer.files;

    // Cancel event and hover styling
    fileDragHover(e);

    // Process all File objects
    for (var i = 0, f; f = files[i]; i++) {
      parseFile(f);
    }
  }

  // Output
  function output(msg) {
    // Response
    var m = document.getElementById('messages');
    m.innerHTML = msg;
  }

  function parseFile(file) {

    console.log(file.name);
    output(
      '<strong>' + encodeURI(file.name) + '</strong>'
    );
    
    // var fileType = file.type;
    // console.log(fileType);
    var imageName = file.name;

    var isGood = (/\.(?=gif|jpg|png|jpeg|jfif)/gi).test(imageName);
    if (isGood) {
      document.getElementById('start').classList.add("hidden");
      document.getElementById('response').classList.remove("hidden");
      // Thumbnail Preview
      document.getElementById('file-image').classList.remove("hidden");
      (<HTMLImageElement>document.querySelector("#file-image")).src= URL.createObjectURL(file);
    //  document.getElementById('file-image').src = URL.createObjectURL(file);
    }
    
  }
  // Check for the various File API support.
  if (window.File && window.FileList && window.FileReader) {
    Init();
  } else {
    document.getElementById('file-drag').style.display = 'none';
  }
}
ekUpload();
  }
  
    
  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
    
  }
  

  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;
      this.member.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    })
  } 

  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe(() => {
      this.member.photos = this.member.photos.filter(x => x.id !== photoId);
    })
  }

  initializeUploader() {
    
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.member.photos.push(photo);
         if (photo.isMain) {
           this.user.photoUrl = photo.url;
           this.member.photoUrl = photo.url;
           this.accountService.setCurrentUser(this.user);
         }
      }
    }
  }

}
