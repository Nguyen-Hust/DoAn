import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-form-builder',
  template: `
    <form [formGroup]="form">
      <ng-container *ngFor="let control of controlNames">
        <div class="form-group mt-3">
          <label class="control-label me-3" [for]="control.name">{{
            control.display
          }}</label>
          <ng-container [ngSwitch]="control.type">
            <input
              *ngSwitchCase="'text'"
              [id]="control.name"
              [formControlName]="control.name"
              placeholder="{{ control.placeholder }}"
              class="form-control"
            />
            <input
              *ngSwitchCase="'number'"
              [id]="control.name"
              [formControlName]="control.name"
              class="form-control"
              placeholder="{{ control.placeholder }}"
              type="number"
            />
            <input
              *ngSwitchCase="'checkbox'"
              [id]="control.name"
              [formControlName]="control.name"
              class="form-check-input"
              type="checkbox"
            />
            <input
              *ngSwitchCase="'date'"
              [id]="control.name"
              [formControlName]="control.name"
              class="form-control"
              placeholder="{{ control.placeholder }}"
              type="date"
            />
            <select
              *ngSwitchCase="'select'"
              [id]="control.name"
              [formControlName]="control.name"
              class="form-control"
              placeholder="control.placeholder"
            >
              <option value="" disabled selected hidden>
                Please Choose...
              </option>
              <option
                *ngFor="let option of control.selectOptions"
                [value]="option.value"
              >
                {{ option.label }}
              </option>
            </select>
            <!-- Add more cases for other control types -->
          </ng-container>
        </div>
      </ng-container>
    </form>
  `,
})
export class FormBuilderComponent {
  @Input() form: FormGroup;
  @Input() controlNames: any[];

  constructor() {}
}
