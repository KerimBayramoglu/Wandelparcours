import { Position } from "./station";
export class Resident {
  id: string;
  firstName: string;
  lastName: string;
  picture: any;
  pictureUrl: any;
  room: string
  birthday: Date
  doctor: Doctor
  images: Images
  videos: Videos
  music: Music
  tags: number[]=new Array();
  lastRecordedPosition: Position = new Position();
  constructor() {
    this.images = new Images();
    this.id = "";
    this.firstName = "";
    this.lastName = "";
    this.room = "";
    this.picture = "";
    this.pictureUrl = "";
    this.birthday = new Date();
    this.doctor = new Doctor();
    this.videos = new Videos();
    this.music = new Music();

  }
}

export class Doctor {
  name: string;
  phoneNumber: string;
  constructor() {
    this.name = "";
    this.phoneNumber = "";
  }
}

export class Images {
  id: string;
  url: string;
  name: string;

  constructor() {
    this.id = "";
    this.url = "";
    this.name = "";
  }
}

export class Music {
    id: string;
    url: string;
    title: string;
    extension: string;

    constructor() {
        this.id = "";
        this.url = "";
        this.title = "";
        this.extension = "";
    }
}

export class Videos {
  id: string;
  url: string;
  type: string;
  name: string;

  constructor() {
    this.id = "";
    this.url = "";
    this.type = "";
    this.name = "";
  }
}

export class ProfilePicture {
  id: string;
  url: string;
  type: string;
  name: string;

  constructor() {
    this.id = "";
    this.id = "";
    this.type = "";
    this.name = "";
  }
}
