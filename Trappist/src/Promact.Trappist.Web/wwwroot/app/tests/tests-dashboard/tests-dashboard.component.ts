import { Component, OnInit, ViewChild } from "@angular/core";
import { MdDialog } from '@angular/material';
import { TestCreateDialogComponent } from "./test-create-dialog.component";
import { DeleteTestDialogComponent } from "./delete-test-dialog.component";
import { TestService } from "../tests.service";
import { Test } from "../tests.model";
import { ActivatedRoute, Router, provideRoutes } from '@angular/router';
import { Http } from "@angular/http";
import { TestSettingsComponent } from "../../tests/test-settings/test-settings.component";
import { TestSettingService } from "../testsetting.service";
import { Response } from "../tests.model";
import { Http } from "@angular/http";

@Component({
    moduleId: module.id,
    selector: "tests-dashboard",
    templateUrl: "tests-dashboard.html"
})

export class TestsDashboardComponent {
    Tests: Test[] = new Array<Test>();
    


    constructor(public dialog: MdDialog, private testService: TestService) {
        this.getAllTests();
    }
    // get All The Tests From Server
    getAllTests() {
        this.testService.getTests().subscribe((response) => { this.Tests = (response), console.log(this.Tests) });
    }
    // open Create Test Dialog
    createTestDialog() {       
            this.dialog.open(TestCreateDialogComponent);
    }
} 
     
@Component({
    moduleId: module.id,
    selector: 'test-create-dialog',
    templateUrl: "test-create-dialog.html"
})

export class TestCreateDialogComponent {
    test_name: string;  
    private errorMessage: boolean;
    testCreateResponse: any;
    teststring: string;
    test: Test = new Test();
    res: Response = new Response;
    t_list: TestsDashboardComponent[] = new Array<TestsDashboardComponent>();
    constructor(private route: ActivatedRoute, public dialog: MdDialog, public testDashboard: TestsDashboardComponent, private testService: TestService) {
    }
    /**
    this method is used to add a new test
    parameter passed is name of the test
    **/
    AddTest(testNameRef: string) {
        this.test.testName = testNameRef;
        this.testService.getTest(this.test.testName).subscribe((response) => {
            this.res = (response);
            console.log(response);
            if (this.res.responseValue) {
                this.errorMessage = false;
                this.testService.addTests("api/addTests", this.test).subscribe((responses) => {
                    this.testCreateResponse = (responses)
                    this.testDashboard.Tests.push(this.testCreateResponse);
                    this.test = new Test();
                    this.dialog.closeAll();
                });
            }
            else 
                this.errorMessage = true;            
        });             
    }
}

