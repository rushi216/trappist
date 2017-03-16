import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";

@Injectable()

export class SetupService {


    constructor(private httpService: HttpService) {

    }

    /**
    validate connection string
    **/
    validateConnectionString(url: string, model: any) {
        return this.httpService.post(url, model);
    }

    /**
    validate email Settings
    **/
    validateEmailSettings(url: string, model: any) {
        return this.httpService.post(url, model);
    }
    /**
     Create User
    **/
    createUser(url: string, model: any) {
        return this.httpService.post(url, model)
    }
}