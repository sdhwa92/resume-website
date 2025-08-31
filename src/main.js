import { careers } from "./career.json";

document.querySelector("#career").innerHTML = `
  ${careers
    .map(
      (career) => `
    <div class="card">
      <div class="career__container">
        <div class="career__heading">
          <div>
            <h2 class="company-name">${career.company}</h2>
            <p class="company-role">${career.role}</p>
          </div>
          <div class="career__date-range">
            <p class="career__date-start">${career.startDate}</p>
            <span>-</span>
            <p class="career__date-end">${career.endDate}</p>
          </div>
        </div>
        <div class="career__description">
          <ul>
            ${career.description
              .map(
                (description) => `
              <li>${description}</li>
            `
              )
              .join("")}
          </ul>
        </div>
      </div>
    </div>
  `
    )
    .join("")}
`;
