# Graduation Project (GOBA)
## Abstract
 Our website aims to provide several customized services to the user, whether the user is a patient or a doctor, as the
 site has several features and advantages.
 There are four main servants**
1. **Diseases Detection:** Some serious lung diseases that are difficult to detect with the naked eye are detected, such as pulmonary fibrosis, tuberculosis and lung cancer. The doctor uploads the x-ray image to the site, and through artificial intelligence and machine learning, it is recognized if the patient has the disease, is healthy, or is exposed to the disease at a certain percentage.
2. **Measuring box:** Through the measurement box, the heart rate, blood oxygen level, and temperature are measured through the finger of the hand, and these percentages are recorded on the site, and they are shown directly on its mobile application.
3. **Medical Advices:** Through medical advices, the doctor can add medical advice and users interact with it, and they can add a comment or add this advice to favorites.
4. **Lung Transplant:** It is a form through which all personal data and all requirements are added that prove that the patient needs a lung transplant, and the validity of this data is detected by artificial intelligence and the authority concerned with lung transplantation to facilitate this process.

## System Architecture
### Onion Architecture
> The Onion architecture is also commonly known as the “Clean architecture” or “Ports and adapters”. These architectural approaches are just variations of the same theme.

### What is the Onion Architecture?
> The Onion architecture is a form of layered architecture and we can visualize these layers as concentric circles. Hence the name Onion architecture. The Onion architecture was first introduced by Jeffrey Palermo, to overcome the issues of the traditional N-layered architecture approach.

There are multiple ways that we can split the onion, but we are going to choose the following approach where we are going to split the architecture into **4 layers:**
1. Domain Layer
2. Service Layer
3. Infrastructure Layer
4. Presentation Layer

Conceptually, we can consider that the Infrastructure and Presentation layers are on the same level of the hierarchy.
Now, let us go ahead and look at each layer with more detail to see why we are introducing it and what we are going to create inside of that layer:
> Image

