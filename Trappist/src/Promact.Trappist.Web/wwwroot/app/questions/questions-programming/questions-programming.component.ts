import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, Validators, FormBuilder, NgForm } from '@angular/forms';
import { CodingLanguage } from "../coding.language.model";
import { CategoryService } from "../categories.service";
import { QuestionsService } from "../questions.service";
import { ProgrammingQuestion } from "../question.programming.model";
import { DifficultyLevel } from "../enum-difficultylevel";
import { Category } from "../category.model";

@Component({
    moduleId: module.id,
    selector: "questions-programming",
    templateUrl: "questions-programming.html"
})

export class QuestionsProgrammingComponent {

    codeSnippetForm: FormGroup;

    selectedCodingLanguages: CodingLanguage[] = new Array<CodingLanguage>();
    categories: Category[] = new Array<Category>();
    codingLanguages: CodingLanguage[] = new Array<CodingLanguage>(); 

    languageInputText: string = "";
    selectedDifficultyLevel: string;
    //questionDetail: string;
    selectedCategory: Category;
    allContentReady: boolean = false;
    noLanguageSelected: boolean = false;

    checkCodeComplexity: boolean = false;
    checkTimeComplexity: boolean = false;
    runBasicTestCase: boolean = false;
    runCornerTestCase: boolean = false;
    runNecessaryTestCase: boolean = false;

    constructor(private categoryService: CategoryService,
        private programmingQuestion: ProgrammingQuestion,
        private questionService: QuestionsService,
        private formBuilder: FormBuilder) {

        this.codingLanguages.sort((a, b) => a.languageCode - b.languageCode);
        this.getCategories();
        this.getCodingLanguages();
        this.selectedCategory = new Category();
        //this.selectedDifficultyLevel = "Easy";

        this.codeSnippetForm = formBuilder.group({
            'categoriesOption': [null, Validators.required],
            'difficultyLevel': ['Easy', Validators.required],
            'questionDetail': ['', Validators.compose([Validators.required, Validators.maxLength(1000)])],
            'languageInput': '',
            'checkCodeComplexity': false,
            'checkTimeComplexity': true,
            'runBasicTestCase': true,
            'runCornerTestCase': false,
            'runNecessaryTestCase': false
        })
    }

    getCategories() {
        this.categoryService.getAllCategories().subscribe((response) => {
            this.categories = response;
            this.allContentReady = true;
        });
    }

    getCodingLanguages() {
        this.questionService.getAllCodingLanguage().subscribe((response) => {
            this.codingLanguages = response;
        });
    }

    selectLanguage(language: CodingLanguage) { 
        this.selectedCodingLanguages.push(language);
        this.codingLanguages.splice(this.codingLanguages.indexOf(language), 1);
        this.languageInputText = null;
        this.validate();
        
    }

    removeSelectedLanguage(selectedLanguage: CodingLanguage) {
        this.codingLanguages.push(selectedLanguage);
        this.selectedCodingLanguages.splice(this.selectedCodingLanguages.indexOf(selectedLanguage), 1);
        this.codingLanguages.sort((a, b) => a.languageCode - b.languageCode);
    }

    selectCategory(category: string) {
        this.selectedCategory = this.categories.find((x) => x.categoryName === category);
    }

    submitForm(form: NgForm) {
        //this.codeSnippetForm
        if (this.validate()) {
            //Map question
            //this.programmingQuestion = this.codeSnippetForm.value;
            this.programmingQuestion.languageList = this.selectedCodingLanguages;
            this.programmingQuestion.categoryId = this.selectedCategory.id;
            //this.programmingQuestion.difficultyLevel = this.difficulties.indexOf(this.codeSnippetForm.controls['difficultyLevel'].value);
            this.programmingQuestion.difficultyLevel = DifficultyLevel[this.codeSnippetForm.controls['difficultyLevel'].value];
            //this.programmingQuestion.questionDetail = this.questionDetail;
            //this.programmingQuestion.questionType = 2; //Type = programming question 
            //this.programmingQuestion.difficultyLevel = DifficultyLevel[this.selectedDifficultyLevel];
            //this.programmingQuestion.categoryId = this.selectedCategory.id;
            //this.programmingQuestion.checkCodeComplexity = this.checkCodeComplexity;
            //this.programmingQuestion.checkCodeComplexity = this.checkTimeComplexity;
            //this.programmingQuestion.runBasicTestCase = this.runBasicTestCase;
            //this.programmingQuestion.runCornerTestCase = this.runCornerTestCase;
            //this.programmingQuestion.runNecessaryTestCase = this.runNecessaryTestCase;
            

            this.questionService.postCodeSnippetQuestion(this.programmingQuestion).subscribe((response) => {
                if (response != null || typeof response != 'undefined') {
                    //To-Do redirect to another page
                    console.log("Added successful");
                }
            });
        }
    }

    validate() {
        if (this.selectedCodingLanguages.length == 0) {
            this.noLanguageSelected = true;

            let element = document.getElementById('codingLanguage');
            element.scrollIntoView(true);

            return false;
        }
        this.noLanguageSelected = false;
        return true;
    }
}
