import {TestBed, inject} from '@angular/core/testing';

import {RestServiceService} from './rest-service.service';
import {StationmanagementComponent} from "../components/stationmanagement/stationmanagement.component";
import {Http, HttpModule, Response} from '@angular/http';
import {RenderBuffer} from "../components/stationmanagement/RenderBuffer";
import {getBaseUrl} from "../app.module.browser";
import {MouseEvents} from "../components/stationmanagement/MouseEvents";
import {Renderer} from "../components/stationmanagement/Renderer";
import {Station} from "../models/station";

describe('RestServiceService', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            providers: [RestServiceService]
        });

    });


    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [RestServiceService]
        });
    });

    it('should be created', inject([RestServiceService], (service: RestServiceService) => {
        expect(service).toBeTruthy();
    }));


    describe("StationManagement test", () => {
        let stationManagemet:StationmanagementComponent;
        beforeEach(inject([RestServiceService], (service: RestServiceService) => {
            stationManagemet=new StationmanagementComponent(service);
            stationManagemet.renderBuffer=new RenderBuffer(stationManagemet);
            
        }));
        
        it('Test load stations error ', inject([RestServiceService], (service: RestServiceService) => {
            service.restUrl = "qsdf";
            let runExpect = (value: boolean) => {
                expect(value).toBeFalsy();
            };
            service.LoadStations(stationManagemet).then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));
        it('Test load stations succesfull ', inject([RestServiceService], (service: RestServiceService) => {
            let runExpect = (value: boolean) => {
                expect(value).toBeTruthy();
            };
            service.LoadStations(stationManagemet).then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));


        it('Test delete stations  error ', inject([RestServiceService], (service: RestServiceService) => {
            let runExpect = (value: boolean) => {
                expect(value).toBeFalsy();
            };
            service.DeleteStation("teststation").then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));

        it('Test insert stations  succes ', inject([RestServiceService], (service: RestServiceService) => {
            let runExpect = (value: boolean) => {
                expect(value).toBeTruthy();
            };
            let station =new Station();
            
            station.mac = "teststation";
            service.SaveStationToDatabase(station).then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));
        it('Test delete stations  success', inject([RestServiceService], (service: RestServiceService) => {
            let runExpect = (value: boolean) => {
                expect(value).toBeTruthy();
            };
            service.DeleteStation("teststation").then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));
        it('Test insert stations  fail ', inject([RestServiceService], (service: RestServiceService) => {
            let runExpect = (value: boolean) => {
                expect(value).toBeFalsy();
            };
            service.restUrl="qsdf";
            let station =new Station();

            station.mac = "teststation";
            service.SaveStationToDatabase(station).then(() => {
                runExpect(true);
            }).catch(() => {
                runExpect(false);

            });
        }));
    });

});
