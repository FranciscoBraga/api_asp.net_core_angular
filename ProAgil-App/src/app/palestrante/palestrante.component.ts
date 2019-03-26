import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-palestrante',
  templateUrl: './palestrante.component.html',
  styleUrls: ['./palestrante.component.css']
})
export class PalestranteComponent implements OnInit {

  @Input() titulo = 'Palestrantes';
  constructor() { }

  ngOnInit() {
  }

}
