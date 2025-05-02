import {Component, OnInit} from '@angular/core';
import {ControlContainer, FormControl, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';
import {FormGroupedSelect, FormSelect} from '../../form.interfaces';
import {Observable, of, startWith} from 'rxjs';

@Component({
  selector: 'app-autocomplete-field',
  standalone: false,
  templateUrl: './autocomplete-field.component.html',
  styleUrls: ['./autocomplete-field.component.scss', '../any-form.component.scss'],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class AutocompleteFieldComponent extends BaseFieldComponent implements OnInit {
  filteredGroupedOptions$!: Observable<FormGroupedSelect[]>;
  filteredOptions$!: Observable<FormSelect[]>;

  getDisplayValue(): string {
    if (this.control?.value) {
      if (typeof this.control.value === 'object') {
        return this.control.value.displayValue || '';
      }
      return this.control.value;
    }
    return '';
  }

  displayFn = (opt: FormSelect | string): string => {
    if (!opt) return '';
    return typeof opt === 'string' ? opt : opt.displayValue;
  };

  get isGroupSelection(): boolean {
    return !!this.field?.options?.[0]?.hasOwnProperty('groupName');
  }

  get groupedOptions(): FormGroupedSelect[] {
    return (this.field.options ?? []) as FormGroupedSelect[];
  }

  get normalOptions(): FormSelect[] {
    return (this.field.options ?? []) as FormSelect[];
  }

  ngOnInit(): void {
    this.filteredGroupedOptions$ = of(this.groupedOptions);
    this.filteredOptions$ = of(this.normalOptions);

    this.control?.valueChanges.pipe(startWith(this.control.value)).subscribe((val: FormSelect | string) => {
      const filterValue = typeof val === 'string' ? val.toLowerCase() : (val?.displayValue ?? '').toLowerCase();

      if (this.isGroupSelection) {
        this.filteredGroupedOptions$ = of(
          this.groupedOptions
            .map(group => ({
              groupName: group.groupName,
              val: group.val.filter(option =>
                option.displayValue.toLowerCase().includes(filterValue)
              )
            }))
            .filter(group => group.val.length > 0)
        );
      } else {
        this.filteredOptions$ = of(
          this.normalOptions.filter(option =>
            option.displayValue.toLowerCase().includes(filterValue)
          )
        );
      }
    });
  }
}
