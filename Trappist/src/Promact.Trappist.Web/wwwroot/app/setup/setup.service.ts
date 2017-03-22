import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";

@Injectable()
export class SetupService {

    constructor(private httpService: HttpService) {
    }

    /**
    This method used for validating connection string
    **/
    validateConnectionString(url: string, model: any) {
        return this.httpService.post(url, model);
    }

    /**
    This method used for verifying email Settings
    **/
    validateEmailSettings(url: string, model: any) {
        return this.httpService.post(url, model);
    }

    /**
    This method used for Creating user
    **/
    createUser(url: string, model: any) {
        return this.httpService.post(url, model)
    }
}