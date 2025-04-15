import {Component, OnInit} from '@angular/core';
import {ControlContainer, FormControl, FormGroupDirective} from '@angular/forms';
import {BaseFieldComponent} from '../base-field.component';
import {FormGroupedSelect, FormSelect} from '../../form.interfaces';
import {Observable, of, startWith} from 'rxjs';

@Component({
  selector: 'app-autocomplete-field',
  standalone: false,
  templateUrl: './autocomplete-field.component.html',
  styleUrl: './autocomplete-field.component.scss',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }]
})
export class AutocompleteFieldComponent extends BaseFieldComponent implements OnInit {
  filteredGroupedOptions$?: Observable<FormGroupedSelect[]>;
  filteredOptions$?: Observable<FormSelect[]>;

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
    this.control?.valueChanges
      .pipe(startWith(''))
      .subscribe(value => {
        const filterValue = value?.toLowerCase() ?? '';

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
