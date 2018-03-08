import { Component, OnInit } from '@angular/core';
import { Resident } from '../../../models/resident';
import { Router, ActivatedRoute } from '@angular/router';
import { RestServiceService } from '../../../service/rest-service.service';

@Component({
  selector: 'app-personalia',
  templateUrl: './personalia.component.html',
  styleUrls: ['./personalia.component.css']
})
export class PersonaliaComponent implements OnInit {
    ngOnInit() { }
    updateResident: any;
    id: string = this.route.snapshot.params['id'];
    resident: Resident;
    router: Router;

    constructor(private service: RestServiceService, private route: ActivatedRoute) {
        this.showOneResident();
        this.resident = <Resident>{ firstName: "", lastName: "", room: "", id: "", birthday: new Date(), doctor: { name: "", phoneNumber: "" } };
    }

    async showOneResident() {
        let resident: any = await this.service.getResidentBasedOnId(this.id);
        console.log(resident);
        if (resident != undefined)
            this.resident = resident;
        else {
            console.log("oops! :( looks like something went wrong :(");
        }
    }
}