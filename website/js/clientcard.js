window.onload  = function(){
    var hashPath = location.hash;
    if (hashPath === '#tutorials'){
      show('Tutorials', 'Home', 'Team');
    }else if (hashPath === '#team'){
      show('Team', 'Home', 'Tutorials');
    } else {
      show('Home', 'Team', 'Tutorials');
    }
    // debugger;
  }
  function show(shown, hidden1, hidden2) {
    document.getElementById(shown.toLowerCase()).classList.add('active');
    document.getElementById(hidden1.toLowerCase()).classList.remove('active');
    document.getElementById(hidden2.toLowerCase()).classList.remove('active');

    document.getElementById(shown).style.display='block';
    document.getElementById(hidden1).style.display='none';
    document.getElementById(hidden2).style.display='none';

    location.hash = show.toLowerCase();
    return false;
  }

  function importHTML(importId, scaffolding) {
    var link = document.getElementById(importId);
    var template = link.import.querySelector('template');
    var clone = document.importNode(template.content, true);

    document.querySelector(scaffolding).appendChild(clone);
  }